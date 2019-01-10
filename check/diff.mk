
check/check:
	@mkdir -p check
	@echo "#!/bin/bash" > check/check
	@echo "diff \"\$$2\" \"\$$3\" --ignore-space" >> check/check
	@chmod +x check/check
