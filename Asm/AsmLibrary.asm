
.data ;We dont store anything inside data section
.code ;Code Section

;compareTest procedure
;It compare values between register xmm1 and xmm6
;If xmm1 register value is greater than xmm6 store 1 in eax register, 0 otherwise
;In our project xmm6 register should contain squared value of expected, "real argument".
;ex. In our project we compare if magnitude is greater than 2, so in this case xmm6 register should contain 4 (2^2)
;xmm1 should contain square magnitude of complex number
;this allows us to buypass problem of different accuracy of root function
;both left and right side have the same exponents so we can drop it and result will stay the same
compareTest proc
xor eax, eax ;set eax to 0
cmpnltsd xmm1, xmm6 ;compare xmm6 with xmm1
;cmpnltsd does not raise flag during operation but stores zeros or ones in destination register based on comparision result
;If we copy those zero or ones to eax register we can use standard comparison to check cmpnltsd result
cvtsd2si eax, xmm1 ;store result as int in eax
;Value below is within eax register if original value of xmm1 is greater than xmm6
cmp eax, 80000000h ;check for specific value in eax register
je comp1 ;if equal go to comp1
jmp comp2 ;if not go to comp2
ret

;Instructions below return value in eax register based on comparision above
comp1: ;break
mov eax, 0 ;return 0
ret
comp2: ;dont break
mov eax, 1 ;return 1
ret
compareTest endp


;magTest, This procedure calculates magnitude of complex number in xmm2 register
;But there is a catch
;Formula for complex number magnitude equals:
; |z|= sqrt(a^2+b^2)
;However given procedure drops root part
; Therefore if we compare result of this procedure We must remember to compare square of it
;This was introduced in order to increase accuracy
;There were some minor differences in square functions between assembly and higher level languages
magTest proc
movapd xmm1, xmm2 ;move xmm2 to xmm1
mulpd xmm1, xmm2 ;multiply xmm1 by xmm2
haddpd xmm1, xmm1 ;horizontal addition of xmm1
ret
magTest endp

;mandelbrot procedure, main procedure of our project
;It takes two complex numbers, iteration limit and escape value
;Procedure checks if first given complex number deviates towards infinity within given iteration limit
;Procedure workflow:
;Square first complex number
;Add the result above to the second complex number
;Calculate complex number magnitude
;Check if it deviates towards infinity
;If yes return current step number
;If no, repeat
mandelbrot proc realOne: REAL8, imagineOne: REAL8, realTwo: REAL8, imagineTwo: REAL8 , limit: DWORD, escape: real8
;realOne a
;imagineOne b
;realTwo c
;imagineTwo d
movlhps xmm0, xmm1 ;store a and b in single xmm register
movlhps xmm2, xmm3 ;store c and d in single xmm register
xor ebx, ebx ;set ebx to 0
xor eax, eax ;set eax to 0
xor ecx,ecx ;set ecx to 0
movlps xmm6, escape ;store escape value in xmm6

loop1: ;main loop for iteration counting
inc cx     ;increment iteration counter
;-------Square------------------------------
movapd xmm1, xmm2 ;xmm1=c+d
movapd xmm3, xmm1 ;xmm3=c+d
shufpd xmm3, xmm1, 1 ;xmm3=d+c
mulpd xmm3, xmm1;xmm3=cd+cd 
haddpd xmm3,xmm3 ;xmm3=2cd

mulpd xmm1, xmm1 ;xmm1=c^2+d^2
shufpd xmm1, xmm2, 1 ;xmm1=d+c^2
mulpd xmm2, xmm2 ;xmm2=c^2+d^2
subpd xmm2, xmm1 ;xmm2=(c^2-d^2)+(d^2-d)
shufpd xmm2, xmm3, 2 ;xmm2=(c^2-d^2)+(2cd)
;-------Add---------------------------------
addpd xmm2, xmm0 ;add first imaginary number to second one
;-------------------------------------------
;-------Magnitude---------------------------
CALL magTest ;calculate magnitude of complex number
;-------------------------------------------
;-------Is break?---------------------------
CALL compareTest
cmp eax, ebx ;compare eax to ebx
je break ;if they are equal go to break
;-------------------------------------------
cmp ecx, limit    ; Compare cx to the limit
jne loop1   ; Loop while less or equal
break:	;break
mov eax, ecx ;store ecx to eax
ret ;return eax
mandelbrot endp
END
;-------------------------------------------------------------------------
