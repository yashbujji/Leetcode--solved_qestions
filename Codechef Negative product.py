
# https://www.codechef.com/problems/NEGPROD

# cook your dish here
T = int(input())
for _ in range(T):
    A,B,C = map(int,input().split())
    if A*B <0 or A*C<0 or B*C <0:
        print('YES')
    else:
        print('NO')
