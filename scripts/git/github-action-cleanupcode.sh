#!/bin/bash

echo ""
echo "--- --- ---"
echo "Alright GitHub Action Cleanup Code Command-Line Tool"
echo "Let's get started, keep calm and wait, it may take few moments"
echo "--- --- ---"
echo ""

jb cleanupcode Blef.sln --profile="Blef: Full Cleanup" --disable-settings-layers=SolutionPersonal --verbosity=WARN

echo ""
echo "--- --- ---"
echo "Git Status"
echo "--- --- ---"
echo ""

git status

echo ""
echo "--- --- ---"
echo "Git Diff"
echo "--- --- ---"
echo ""

git diff