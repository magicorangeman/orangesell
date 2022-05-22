from aiogram import types, Dispatcher
from create_bot import dp, bot
from keyboards import kb_client
from aiogram.types import ReplyKeyboardRemove
from data_base import sqlite_db

#@dp.message_handler(commands=['start', 'help'])
async def command_start(message : types.Message):
    try:
        await bot.send_message(message.from_user.id, 'Я жажду служить!', reply_markup=kb_client)
        await message.delete()
    except:
        await message.reply('Общение с ботом через ЛС, напишите ему:\nhttps://t.me/Pandamax_bot')

#@dp.message_handler(commands=['Повелеваю'])
async def service(message : types.Message):
    await bot.send_message(message.from_user.id, 'Склоняюсь перед вашей волей!', reply_markup=ReplyKeyboardRemove())

#@dp.message_handler(commands=['Меню'])
async def menu_command(message : types.Message):
    await sqlite_db.sql_read(message)


def register_handlers_client(dp : Dispatcher):
    dp.register_message_handler(command_start, commands=['start', 'help'])
    dp.register_message_handler(service, lambda message: 'Повелеваю' in message.text)
    dp.register_message_handler(menu_command, lambda message: 'Меню' in message.text)