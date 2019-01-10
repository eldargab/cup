

check/check: check/Check.java
	@mkdir -p check/lib
	@javac -d check/lib -classpath check/testlib4j.jar check/Check.java
	@echo "#!/bin/bash" > check/check
	@echo "java -cp check/testlib4j.jar:check/lib ru.ifmo.testlib.CheckerFramework Check \"\$$@\"" >> check/check
	@chmod +x check/check
