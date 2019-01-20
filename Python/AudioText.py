import speech_recognition as sr

mic = sr.Microphone()
r = sr.Recognizer()
testString = "where is my"
string = ""

with mic as source:
    r.adjust_for_ambient_noise(source)
    audio = r.listen(source)

try:
    string = r.recognize_google(audio)
    #print("TEXT: " + string)
    if testString in string:
        leftString = string.split(testString,3)[1]
        print(leftString)
        #print (leftString.split(" ")[1])

except:
    pass
