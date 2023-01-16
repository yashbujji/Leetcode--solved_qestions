# https://www.codechef.com/problems/FINE

# cook your dish here
T = int(input())
for _ in range(T):
    X = int(input())
    if X <= 70:
        print(0)
    elif X <= 100:
        print(500)
    else:
        print(2000)
