
out/check: Makefile check/Check.java
	@mkdir -p out/checker
	@javac -d out/checker -classpath check/testlib4j.jar check/Check.java
	@echo "#!/bin/bash" > out/check
	@echo "java -cp check/testlib4j.jar:out/checker ru.ifmo.testlib.CheckerFramework Check \"\$$@\"" >> out/check
	@chmod +x out/check
