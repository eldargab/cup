BUILD=out/program.jar
RUN=java -Xmx256M -Xss64M -cp out/program.jar Main


out/program.jar: src/*
	@mkdir -p out/classes
	@javac -d out/classes src/*
	@cd out/classes; jar cf ../program.jar *.class


cleanup:
	@rm -rf out