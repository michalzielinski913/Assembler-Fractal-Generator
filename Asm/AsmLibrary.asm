
.data
.code
compareTest proc
xor eax, eax ;set eax to 0
cmpnltsd xmm1, xmm6 ;compare xmm6 with xmm1
cvtsd2si eax, xmm1 ;store result as int in eax
cmp eax, 80000000h ;check for specific value in eax register
je comp1 ;if equal go to comp1
jmp comp2 ;if not go to comp2
ret

comp1: ;break
mov eax, 0 ;return 0
ret
comp2: ;dont break
mov eax, 1 ;return 1
ret
compareTest endp

magTest proc
movapd xmm1, xmm2 ;move xmm2 to xmm1
mulpd xmm1, xmm2 ;multiply xmm1 by xmm2
haddpd xmm1, xmm1 ;horizontal addition of xmm1
ret
magTest endp


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
