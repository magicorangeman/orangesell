import datetime as dt
print('Введите дату рабочей смены')
day = input('Число: ')
month = input('Месяц: ')
year = input('Год: ')
today = dt.datetime(int(year), int(month), int(day))
print(f'Указанный рабочий день: {today.strftime("%D")}')
print('Последующие рабочие смены в этом году будут:')
time = today
while int(time.strftime('%Y')) < 2022:
    time += dt.timedelta(days=4)
    print(time.strftime("%D"))
input()