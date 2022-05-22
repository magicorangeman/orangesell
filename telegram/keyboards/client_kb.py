from aiogram.types import ReplyKeyboardMarkup, KeyboardButton#, ReplyKeyboardRemove

b1 = KeyboardButton('Повелеваю')
b2 = KeyboardButton('Меню')
b3 = KeyboardButton('Звякнуть мне', request_contact=True)
b4 = KeyboardButton('Аудиенция жреца', request_location=True)

kb_client = ReplyKeyboardMarkup(resize_keyboard=True)

kb_client.add(b1).row(b2).insert(b3).row(b4)