BUILD=bin/Program.dll
RUN=mono bin/Program.dll
FILES=$(wildcard *.cs)


bin/Program.dll: $(FILES)
	@mkdir -p bin
	@mcs -langversion:7 -out:bin/Program.dll $(FILES)


cleanup:
	@rm -rf bin/Program.dll