# !/usr/local/bin/python3
# coding=utf-8
import random
import json
import requests
import datetime
import time
import os


# def AppendLog(content):
#     location = 'D:/Publish/src/YijuServ/data/'a
#     f = open(location + "_LOG_Production_.txt", "a+")
#     localtime = time.asctime(time.localtime(time.time()))
#     f.write(localtime + "\n" + content + "\n")
#     f.close()


def postReq(url, content):
    headers = {'Content-Type': 'application/json'}
    x = requests.post(url, data=content, headers=headers)
    return x.text


def getAnonymousToken():
    url = 'https://api.yjezimoc.com/token/login'
    obj = '\"{\'usr\':\'anonymous\',\'pwd\':\'188888\',\'hd\':\'188888\'}"'
    x = postReq(url, obj)
    return x


def getAccessToken(appId, secret, siteId):
    url = 'https://api.yjezimoc.com/token/login'
    obj = '\"{\'usr\':\'' + appId + '\',\'pwd\':\'' + \
        secret + '\',\'hd\':\'' + siteId + '\'}"'
    x = postReq(url, obj)
    return x


# def searchListing(token):
#     if (len(token) > 20):
#         url = 'http://api.yjezimoc.com/search/map'
#         mt0 = 'a;'
#         mt1 = ';1;dF;2;=;43.621584_-79.624480|43.668424_-79.58430'
#         pagesize = random.randint(1, 9)
#         mt = mt0 + str(pagesize) + mt1
#         body = '\"{\'tk\':\'' + token + '\',\'mt\':\'' + \
#             mt + '\',\'st\':\'188888\'}"'
#         # print(body)

#         start = time.time()
#         data = postReq(url, body)
#         end = time.time()
#         log1 = str(pagesize) + ' : Received ' + str(len(data)) + ' bytes'
#         log2 = 'Cost ' + str((int)((end - start) * 1000)) + ' ms'
#         log = log1 + '\n' + log2 + '\n'
#         print(log)
#         AppendLog(log)
#     else:
#         error = "CONNECT ERROR OR LOGIN FAILED.\n"
#         print(error)
#         AppendLog(error)


if __name__ == "__main__":
    tk = getAccessToken(
        'i@tabtu.cn', 'b5288f12c9a5881aab361704cd1873df', '188888')
    print(tk)
