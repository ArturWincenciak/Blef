#!/bin/bash

echo ""
echo "--- --- ---"
echo "Alright Cleanup Code Command-Line Tool"
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
    exit 0
fi

STAGED_UNCOMMITTED=`git diff --staged --name-only`
if [ -z "$STAGED_UNCOMMITTED" ]
then
    echo ""
    echo "--- --- ---"
    echo "Right, there are no staged, uncommitted changes"
    echo "--- --- ---"
    echo ""
else
    echo ""
    echo "--- --- ---"
    echo "There are no staged, uncommitted changes"
    echo "Commit them before run the script"
    echo "--- --- ---"
    echo ""

    git diff --staged --name-only
    exit 0
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
    echo "No files re-formated"
    echo "--- --- ---"
    echo ""
    exit 0
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