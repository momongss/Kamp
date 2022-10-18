#!/bin/sh
# 수정된 파일을 모두 commit 후 push 합니다.
# commit message는 input으로 받습니다.

message=""

if [ "$1" = "" ]
then message="Tongchun is too busy to write commit message."
else message=$1
fi

git add .
git commit -m "$message"
git push -u origin sohyun

read name