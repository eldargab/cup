out/run: Makefile Program.cs
	@mkdir -p out
	@mcs -langversion:5 -out:out/Program.dll Program.cs
	@echo "#!/bin/bash" > out/run
	@echo "mono out/Program.dll \"\$$@\"" >> out/run
	@chmod +x out/run
