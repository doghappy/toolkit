#!zsh
days="1"
time="16:30:00"

helpFunction() {
    print -P "%F{reset_color}"
    echo "Usage:"
    echo -e "\t-d How many days ago the code needs to be reviewed, default is 1"
    echo -e "\t-t Start time of code review, default is 16:30:00"
    echo -e "\t-n Git repo name"
    exit 1
}

while getopts "d:t:n:" opt; do
    case "$opt" in
    d) pDays="$OPTARG" ;;
    t) pTime="$OPTARG" ;;
    n) pName="$OPTARG" ;;
    ?) helpFunction ;;
    esac
done

if [ ! -z "$pDays" ]; then
    days=$pDays
fi

if [ ! -z "$pTime" ]; then
    time=$pTime
fi

if [ -z "$pName" ]; then
    print -P "%F{yellow}Which repo do you want to review?"
    helpFunction
else
    repo=$pName
fi

regex="(20[[:digit:]]{2}-[[:digit:]]{2}-[[:digit:]]{2}[[:blank:]]{1}[[:digit:]]{2}:[[:digit:]]{2}:[[:digit:]]{2})[[:blank:]]{1}-[[:blank:]]{1}(\[[[:alnum:][:blank:]&]+\])[[:blank:]]*#[[:blank:]]*([[:alnum:]]{2,6})[[:blank:]]*(.*)"
cards=()
git -C ${repo} log --pretty=format:"%ad - %s" --after="$(date -v-${days}d +'%Y-%m-%dT${time}')" --date=format:'%Y-%m-%d %H:%M:%S' | while IFS= read -r line; do
    if [[ $line =~ $regex ]]; then
        card=$match[3]

        exists=false
        for i in "${cards[@]}"; do
            if [ "$i" -eq "$card" ]; then
                exists=true
                break
            fi
        done

        if $exists; then
            print -P "%F{gray}$line"
        else
            cards+=($card)
            print -P "%F{green}$line"
        fi
    else
        print -P "%F{red}$line"
    fi
done
