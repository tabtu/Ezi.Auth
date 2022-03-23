# !/usr/local/bin/python3
# coding=utf-8
import requests


class Req:
    def getSHA1(self, token, timestamp, nonce, encrypt):


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
        obj = '\"{\'usr\':\'anonymous\',\'pwd\':\'100001\',\'st\':\'100001\',\'hd\':\'192.168.1.1\'}"'
        x = postReq(url, obj)
        return x

    def getAccessToken(appId, secret, siteId, binding):
        url = 'https://api.yjezimoc.com/token/login'
        obj = '\"{\'usr\':\'' + appId + '\',\'pwd\':\'' + secret + '\',\'st\':\'' + siteId + '\',\'st\':\'' + binding + '\'}"'
        x = postReq(url, obj)
            return x


    if __name__ == "__main__":
        tk = getAccessToken('appId', 'secret', 'siteId')
        print(tk)
