#!/bin/bash
city=$1
days=$2

# Определение пути к каталогу скрипта
SCRIPT_DIR=$(dirname "$(realpath "$0")")

# Переход в каталог проекта
cd "$SCRIPT_DIR" || exit

dotnet run -- "$city" "$days"