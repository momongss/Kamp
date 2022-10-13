#!/bin/sh
# 수정된 파일을 모두 commit 후 push 합니다.
# commit message는 input으로 받습니다.

read message

git add .
git commit -m "$message"
git push -u origin main
