from aiogram import Bot, types
from aiogram.dispatcher import Dispatcher
from aiogram.utils import executor
import os

bot = Bot(token=os.getenv('TOKEN'))
dp = Dispatcher(bot)

@dp.message_handler(commands=['start', 'help'])
async def command_start(message: types.Message):
    await message.reply('Опять работа?')

@dp.message_handler(commands=['Пади ниц!'])
async def echo(message: types.Message):
    await message.answer(message.text)

@dp.message_handler(lambda message: 'такси' in message.text)
async def taxi(message: types.Message):
    await message.answer('такси')

@dp.message_handler(lambda message: message.text.startwith('такси'))
async def taxi_plus(message: types.Message):
    await message.answer(message.text[6:])

@dp.message_handler()
async def empty(message: types.Message):
    await message.answer('Ну вот, меня убъют')
    await message.delete()

executor.start_polling(dp, skip_updates=True)