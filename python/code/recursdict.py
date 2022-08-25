
def rec(sl, search):
    if search in sl.keys():
        return sl[search]

    for n in sl.values():
        if type(n) = dict:
            ret = rec(n, search)
            if ret != None:
                return ret



print(rec(title, input('Enter \n')))