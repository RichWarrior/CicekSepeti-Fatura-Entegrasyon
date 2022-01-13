#!/bin/sh
# $0 is the script name, $1 id the first ARG, $2 is second...
NAME="$1"
OUTPUT="$2"
wkhtmltopdf $NAME $OUTPUT
echo "PDF Export OK"