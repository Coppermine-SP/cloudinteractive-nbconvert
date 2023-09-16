#!/bin/sh

mkdir ./linux-x64
cp -r ./linux-x64/* ./package/usr/lib/cloudinteractive-nbconvert
chmod -R 0755 ./package/
dpkg -b package