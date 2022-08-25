import sys
def some():
    return
    print('член')
    return  (some)

def factorial(num):
    if num <= 1:
        return 1
    else:
        return num * factorial(num-1)

def some2(num):
    result = 1
    count = 1
    while count <= num:
        result *= count
        count += 1
    return result

n = input('Глубина рекурсии: ')
sys.setrecursionlimit(n)
print(sys.getrecursionlimit())

x = factorial(int(input('Введите число: ')))

print (x)