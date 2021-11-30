.data
.code
compareTest proc
xor eax, eax
cmpnltsd xmm1, xmm6
cvtsd2si eax, xmm1
cmp eax, 80000000h
je comp1
jmp comp2
ret

comp1: ;break
mov eax, 0
ret
comp2: ;dont break
mov eax, 1
ret
compareTest endp

comSquare proc
movapd xmm1, xmm0
shufpd xmm1, xmm1, 1
movapd xmm2, xmm0
mulpd xmm0, xmm2 ;ac, bd
;test PMADDWD or DPPS
mulpd xmm1, xmm2 ;ad, bc
;because bd has i^2 which equals -1 we must invert sign by subtracting its value twice
shufpd xmm0, xmm0, 1; bd, ac
movapd xmm4, xmm0 ;save bd, ac to xmm4
subsd xmm4, xmm0 ;subtract xmm0 from xmm4
subsd xmm4, xmm0 ;subtract xmm0 from xmm4
movapd xmm0, xmm4 ;save xmm4 back to xmm0
;-------------------------------------------
shufpd xmm0, xmm0, 1 ;ac, bd
haddpd xmm0, xmm0 ;ac+bd
haddpd xmm1, xmm1 ;ad+bc
shufpd xmm0, xmm1, 1bh ;(ac+bd)+(ad+bc)i
comSquare endp



square proc
;multiply (a+bi)*(c+di)
movapd xmm1, xmm0
shufpd xmm1, xmm1, 1

movapd xmm2, xmm0

mulpd xmm0, xmm2 ;ac, bd
;test PMADDWD or DPPS
mulpd xmm1, xmm2 ;ad, bc
;because bd has i^2 which equals -1 we must invert sign by subtracting its value twice
shufpd xmm0, xmm0, 1; bd, ac
movapd xmm4, xmm0 ;save bd, ac to xmm4
subsd xmm4, xmm0 ;subtract xmm0 from xmm4
subsd xmm4, xmm0 ;subtract xmm0 from xmm4
movapd xmm0, xmm4 ;save xmm4 back to xmm0
;-------------------------------------------
shufpd xmm0, xmm0, 1 ;ac, bd
haddpd xmm0, xmm0 ;ac+bd
haddpd xmm1, xmm1 ;ad+bc
shufpd xmm0, xmm1, 1bh ;(ac+bd)+(ad+bc)i
ret
square endp

magTest proc
movapd xmm1, xmm0
mulpd xmm1, xmm0
haddpd xmm1, xmm1
sqrtsd xmm1, xmm1
ret
magTest endp


mandelbrot proc realOne: REAL8, imagineOne: REAL8, realTwo: REAL8, imagineTwo: REAL8 , limit: DWORD, escape: real8
xor ebx, ebx
xor eax, eax
xor ecx,ecx   ; cx-register is the counter, set to 0
movlps xmm6, escape
movlps xmm0, realOne ;load a
movhps xmm0, imagineOne ;load b
loop1:
inc cx      ; Increment
;-------Square------------------------------
CALL square
;-------------------------------------------
;-------Add---------------------------------
;
;-------------------------------------------
;-------Magnitude---------------------------
CALL magTest
;-------------------------------------------
;-------Is break?---------------------------
CALL compareTest
cmp eax, ebx
je break
;-------------------------------------------
cmp ecx, limit    ; Compare cx to the limit
jne loop1   ; Loop while less or equal
break:
mov eax, ecx
ret
mandelbrot endp


loopTest proc iterationlimit: DWORD
xor ebx, ebx
xor eax, eax
xor ecx,ecx   ; cx-register is the counter, set to 0
loop1:
add eax, 1
nop         ; Whatever you wanna do goes here, should not change cx
inc cx      ; Increment
cmp ecx,iterationlimit    ; Compare cx to the limit
jne loop1   ; Loop while less or equal
break:
ret
loopTest endp
END
;-------------------------------------------------------------------------
