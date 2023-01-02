#!/bin/bash

echo ""
echo "--- --- ---"
echo "Alright GitHub Action Cleanup Code Command-Line Tool"
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
