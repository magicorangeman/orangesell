import requests
from datetime import datetime
import telebot
from config import TOKEN


def get_data():
    req = requests.get()
    response = req.json()
    sell_price = response["btc_usd"]["sell"]
    print(f"{datetime.now().strftime('%Y-%m-%d %H:%M')}\nSell BTC price: {sell_price}")

def telegram_bot(TOKEN):
    bot = telebot.TeleBot(TOKEN)
    @bot.message_handler(commands=['start'])
    def start_message(message):
        bot.send_message(message.chat.id, "Приветствую!")

    bot.polling()




if __name__ == "__main__":
#   get_data()
    telegram_bot(TOKEN)