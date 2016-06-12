NUGET ?= nuget
MONO ?= mono
FAKE ?= packages/FAKE.4.28.0/tools/FAKE.exe

all: dependencies install

dependencies:
	${NUGET} install set/packages.config -o packages

install:
	${MONO} ${FAKE} build.fsx

.PHONY:	dependencies install
