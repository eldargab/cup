out/run: Makefile src/*
	@mkdir -p out/main
	@javac -d out/main src/*
	@cd out/main; jar cf ../main.jar *.class
	@echo "#!/bin/bash" > out/run
	@echo "java -Xmx256M -Xss64M -cp out/main.jar Main \"\$$@\"" >> out/run
	@chmod +x out/run
