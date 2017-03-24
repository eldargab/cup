#!/bin/bash

pushd `dirname $0` > /dev/null
SCRIPTDIR=`pwd -P`
popd > /dev/null

function abort {
  test -n "$1" && echo $1
  exit 1
}

DIR=$1
PROJECT_TYPE=$2
TESTS=$3

function init_project {
  case $PROJECT_TYPE in
    java)
      cp -R "$SCRIPTDIR/java" "$DIR" || abort
      cp "$SCRIPTDIR/java.gitignore" "$DIR/.gitignore" || abort
      ;;
    *)
      abort "Error: Unknown project type: $PROJECT_TYPE";;
  esac
}

if [ -z "$DIR" ] || [ -z "$PROJECT_TYPE" ]
  then abort "Usage: init <target_dir> <project_type> [test_cases]"
fi

if [ -e "$DIR" ]; then
  echo -n "Directory $DIR already exists and will be erased. Continue? [y/N] "
  read ans
  if [ "$ans" != "y" ]
      then exit 1
  fi
  rm -rf "$DIR"
fi

init_project

if [ -n "$TESTS" ]; then
  cp -R "$TESTS/tests" "$DIR/tests" || abort
  if [ -e "$TESTS/Check.java" ]; then
    mkdir "$DIR/check" || abort
    cp "$SCRIPTDIR/check/testlib4j.jar" "$TESTS/Check.java" "$DIR/check" || abort
    cat "$SCRIPTDIR/check/java.mk" >> "$DIR/Makefile" || abort
  elif [ -e "$TESTS/check.cpp" ]; then
    mkdir "$DIR/check" || abort
    cp "$SCRIPTDIR/check/testlib.h" "$TESTS/check.cpp" "$DIR/check" || abort
    cat "$SCRIPTDIR/check/cpp.mk" >> "$DIR/Makefile" || abort
  else
    cat "$SCRIPTDIR/check/diff.mk" >> "$DIR/Makefile" || abort
  fi
else
  cat "$SCRIPTDIR/check/diff.mk" >> "$DIR/Makefile" || abort
  mkdir -p "$DIR/tests" || abort
  touch "$DIR/tests/01" || abort
  touch "$DIR/tests/01.a" || abort
fi

cat "$SCRIPTDIR/common.mk" >> "$DIR/Makefile"
