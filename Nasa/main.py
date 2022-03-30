import pathlib

import requests
import os
from bs4 import BeautifulSoup as bs
import urllib.request as ulib
from selenium import webdriver

page = 0
driver = webdriver.Firefox(executable_path="C:\geckodriver\geckodriver.exe")
path = r"D:/Test"
currentCamera = "chemcam"


def get_picture_urls(url):
    urls = []
    driver.get(url)
    for x in driver.find_elements_by_tag_name('img'):
        src = x.get_attribute('src')
        if "2_PIA14175" not in src and "logo" not in src and "arrow" not in src and "320x240" not in src and "icon" not in src:
            urls.append(src.replace('-thm', ''))

    return urls


if __name__ == '__main__':
    baseUrl = r"https://mars.nasa.gov/msl/multimedia/raw-images/?order=sol+desc%2Cinstrument_sort+asc%2Csample_type_sort+asc%2C+date_taken+desc&per_page=100&page="
    cameras = {"fhazcam": "&af=FHAZ_RIGHT_A%7CFHAZ_LEFT_A%7CFHAZ_RIGHT_B%7CFHAZ_LEFT_B%2C%2C%2C",
               "rhazcam": "&af=RHAZ_RIGHT_A%7CRHAZ_LEFT_A%7CRHAZ_RIGHT_B%7CRHAZ_LEFT_B%2C%2C%2C",
               "nleftcam": "&af=NAV_LEFT_A%7CNAV_LEFT_B%2C%2C%2C",
               "nrightcam": "&af=NAV_RIGHT_A%7CNAV_RIGHT_B%2C%2C%2C",
               "chemcam": "&af=CHEMCAM_RMI%2C%2C",
               "mardi": "&af=MARDI%2C%2C",
               "mahli": "&af=MAHLI%2C%2C",
               "mastcam": "&af=MAST_LEFT%7CMAST_RIGHT%2C%2C"}
    print(baseUrl + str(
        page) + "&mission=msl" + cameras.get(currentCamera))
    currentImage = 0
    imageAmount = 1
    while (imageAmount > 0):
        urls = get_picture_urls(
            baseUrl + str(
                page) + "&mission=msl" + cameras.get(currentCamera))
        imageAmount = len(urls)
        for x in urls:
            img_data = requests.get(x).content
            with open(path + "/" + currentCamera + "/" + str(currentImage) + ".jpg", 'wb') as handler:
                handler.write(img_data)
                currentImage += 1
                print("ImageSaved: " + str(currentImage))
        else:
            page += 1
            print(page)
    driver.close()