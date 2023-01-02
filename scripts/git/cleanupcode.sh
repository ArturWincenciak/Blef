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
echo "Alright Cleanup Code Command-Line Tool"
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
echo "- fail on re-format needed: $FAIL_ON_REFORMAT_NEEDED"
echo "- auto commit re-formated code: $AUTO_COMMIT"
if [ $FAIL_ON_REFORMAT_NEEDED == "yes" ] && [ $AUTO_COMMIT == "yes" ]; then
	echo "NOTICE: you have set that the execution will fast fail on re-format needed"
	echo "NOTICE: auto commit will not be executed because the execution will terminate with fail when re-format is needed"
	echo "NOTICE: if you want to auto commit execute call the script with '-f no -a yes' arguments"
fi
echo "--- --- ---"
echo ""

UNSTAGED_CHANGES=`git diff --name-only`
if [ -z "$UNSTAGED_CHANGES" ]
then
    echo ""
    echo "--- --- ---"
    echo "Right, there are no unstaged changes"
    echo "--- --- ---"
    echo ""
else
    echo ""
    echo "--- --- ---"
    echo "There are unstaged changes"
    echo "Commit them before run the script"
    echo "--- --- ---"
    echo ""

    git diff --name-only
    exit $YOU_NEED_NO_CHANGES_BEFORE_RUN_CLEANUP_ERROR
fi

STAGED_UNCOMMITTED=`git diff --staged --name-only`
if [ -z "$STAGED_UNCOMMITTED" ]
then
    echo ""
    echo "--- --- ---"
    echo "Right, there is no any changes, repo is ready to cleanup"
    echo "--- --- ---"
    echo ""
else
    echo ""
    echo "--- --- ---"
    echo "There are staged, uncommitted changes"
    echo "Commit them before run the script"
    echo "--- --- ---"
    echo ""

    git diff --staged --name-only
    exit $YOU_NEED_NO_CHANGES_BEFORE_RUN_CLEANUP_ERROR
fi

echo ""
echo "--- --- ---"
echo "Let's get started, keep calm and wait, it may take few moments"
echo "--- --- ---"
echo ""

jb cleanupcode Blef.sln --profile="Blef: Full Cleanup" --disable-settings-layers=SolutionPersonal --verbosity=WARN

REFORMATED_FILES=`git diff --name-only`

if [ -z "$REFORMATED_FILES" ]
then
    echo ""
    echo "--- --- ---"
    echo "No files re-formated, everything is clean, congratulation!"
    echo "--- --- ---"
    echo ""
    exit $SUCCESS
fi

if [ $FAIL_ON_REFORMAT_NEEDED == "yes" ]
then
    echo ""
    echo "--- --- ---"
    echo "Exit with re-formated code needed fail status"
    echo "--- --- ---"
    echo ""
    exit $FAIL_ON_REFORMAT_NEEDED
fi

if [ $AUTO_COMMIT == "no" ]
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
echo "There are re-formated files to be committed"
echo "--- --- ---"
echo ""

git diff --name-only

for FILE in "${REFORMATED_FILES[@]}"
do
    git add $FILE
done

echo ""
echo "--- --- ---"
echo "Staged files to be committed"
echo "--- --- ---"
echo ""

git diff --staged --name-only

echo ""
echo "--- --- ---"
echo "Create commit"
echo "--- --- ---"
echo ""

git commit -m "Re-format code by JetBrains CleanupCode Tool"

echo ""
echo "--- --- ---"
echo "Commit created"
echo "--- --- ---"
echo ""

git status

echo ""
echo "--- --- ---"
echo "All re-formated code has been commited with success"
echo "--- --- ---"
echo ""
exit $SUCCESS