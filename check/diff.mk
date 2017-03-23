
out/check:
	@mkdir -p out
	@echo "#!/bin/bash" > out/check
	@echo "diff $2 $3 --ignore-space" >> out/check
	@chmod +x out/check
