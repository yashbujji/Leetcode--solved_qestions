# https://www.codechef.com/problems/FARFROMO 

# cook your dish here
t = int(input())

for _ in range(t):
    x1, y1, x2, y2 = map(int, input().split())
    d1,d2 = (x1**2 + y1**2)**0.5,(x2**2 + y2**2)**0.5
    # d2 = (x2**2 + y2**2)**0.5
    if d1 < d2:
        print("BOB")
    elif d1 > d2:
        print("ALEX")
    else:
        print("EQUAL")

