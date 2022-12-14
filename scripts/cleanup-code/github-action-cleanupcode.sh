#!/bin/bash

# Exit codes
SUCCESS=0
INVALID_ARGUMENT_ERROR=1
EXIT_WITH_FAST_FAIL=2
YOU_NEED_NO_CHANGES_BEFORE_RUN_CLEANUP_ERROR=3

# Default arguments' values
FAIL_ON_REFORMAT_NEEDED=no
AUTO_COMMIT=yes

echo ""
echo "--- --- ---"
echo "Alright GitHub Action Cleanup Code Command-Line Tool"
echo "Default settings:"
echo "- fail on re-format needed (-f): '$FAIL_ON_REFORMAT_NEEDED'"
echo "- auto commit re-formated code (-a): '$AUTO_COMMIT'"
echo "--- --- ---"
echo ""

while getopts f:a: flag
do
    case "${flag}" in
        f) FAIL_ON_REFORMAT_NEEDED=${OPTARG};;
        a) AUTO_COMMIT=${OPTARG};;
        *) echo ""
           echo "--- --- ---"
           echo "Invalid argument's flag is not handled"
           echo "--- --- ---"
           echo ""
           exit $INVALID_ARGUMENT_ERROR ;;
    esac
done

if [ $FAIL_ON_REFORMAT_NEEDED != "yes" ] && [ $FAIL_ON_REFORMAT_NEEDED != "no" ]
then
    echo ""
    echo "--- --- ---"
    echo "INVALID ARGUMENT OF '-f' equals '$FAIL_ON_REFORMAT_NEEDED'"
    echo "Set 'yes' or 'no' or omit to use default equals 'no'"
    echo "--- --- ---"
    echo ""
    exit $INVALID_ARGUMENT_ERROR
fi

if [ $AUTO_COMMIT != "yes" ] && [ $AUTO_COMMIT != "no" ]
then
    echo ""
    echo "--- --- ---"
    echo "INVALID ARGUMENT OF '-a' equals '$AUTO_COMMIT'"
    echo "Set 'yes' or 'no' or omit to use default equals 'no'"
    echo "--- --- ---"
    echo ""
    exit $INVALID_ARGUMENT_ERROR
fi

echo ""
echo "--- --- ---"
echo "Your setup:"
echo "- fail on re-format needed: '$FAIL_ON_REFORMAT_NEEDED'"
echo "- auto commit re-formated code: '$AUTO_COMMIT'"
if [ $FAIL_ON_REFORMAT_NEEDED = "yes" ] && [ $AUTO_COMMIT = "yes" ]
then
    echo "NOTICE: you have set that the execution will fast fail on re-format needed"
    echo "NOTICE: auto commit will not be executed because the execution will terminate with fail when re-format is needed"
    echo "NOTICE: if you want to auto commit execute call the script with '-f no -a yes' arguments"
fi
echo "--- --- ---"
echo ""

echo ""
echo "--- --- ---"
echo "Let's get started, keep calm and wait, it may take few moments"
echo "--- --- ---"
echo ""

dotnet tool restore
dotnet tool update --global JetBrains.ReSharper.GlobalTools
jb cleanupcode Blef.sln --profile="Blef: Full Cleanup" --disable-settings-layers=SolutionPersonal --verbosity=WARN

REFORMATED_FILES=$(git diff --name-only)

if [ -z "$REFORMATED_FILES" ]
then
    echo ""
    echo "--- --- ---"
    echo "No files re-formated, everything is clean, congratulation!"
    echo "--- --- ---"
    echo ""
    exit $SUCCESS
fi

if [ $FAIL_ON_REFORMAT_NEEDED = "yes" ]
then
    echo ""
    echo "--- --- ---"
    echo "Exit with re-formated code needed fail status"
    echo "--- --- ---"
    echo ""
    exit $EXIT_WITH_FAST_FAIL
fi

if [ $AUTO_COMMIT = "no" ]
then
    echo ""
    echo "--- --- ---"
    echo "There is re-formated code but it will not be auto commited"
    echo "--- --- ---"
    echo ""
    exit $SUCCESS
fi

echo ""
echo "--- --- ---"
echo "There is re-formated code to be committed"
echo "--- --- ---"
echo ""

git diff --name-only

echo ""
echo "--- --- ---"
echo "Git Diff"
echo "--- --- ---"
echo ""

git diff

echo ""
echo "--- --- ---"
echo "Add all changes to stage"
echo "--- --- ---"
echo ""

git add .

echo ""
echo "--- --- ---"
echo "Staged files to be committed"
echo "--- --- ---"
echo ""

git diff --staged --name-only

echo ""
echo "--- --- ---"
echo "Creating commit"
echo "--- --- ---"
echo ""

git config --global user.email "github@action.com"
git config --global user.name "CleanupCode GitHub Action"
git commit -m "GitHub Action: re-format code by JetBrains CleanupCode tool"

echo ""
echo "--- --- ---"
echo "Commit has been created"
echo "--- --- ---"
echo ""

git status

echo ""
echo "--- --- ---"
echo "Push the commit"
echo "--- --- ---"
echo ""

git push

echo ""
echo "--- --- ---"
echo "Commit has been pushed"
echo "--- --- ---"
echo ""

git status

echo ""
echo "--- --- ---"
echo "All re-formated code has been commited and pushed with success"
echo "--- --- ---"
echo ""
exit $SUCCESS
