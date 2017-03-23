
test%: tests/%.a out/run
	@cat tests/$*
	@echo
	@echo
	@cat tests/$* | out/run
	@echo
	@cat tests/$*.a
	@echo

test: test01

run%: tests/%.a out/run
	@cat tests/$* | out/run > tests/$*.result

time%: tests/%.a out/run
	@echo Test $*
	@time cat tests/$* | out/run > tests/$*.result
	@echo

grade%: tests/%.a out/run out/check
	@cat tests/$* | out/run > tests/$*.result && out/check tests/$* tests/$*.result tests/$*.a

time:  $(patsubst tests/%.a, time%, $(wildcard tests/*.a))
grade: $(patsubst tests/%.a, grade%, $(wildcard tests/*.a))

clean:
	@rm -rf out
	@rm -f tests/*.result

.PHONY: clean test time grade
.PHONY: $(patsubst tests/%.a, test% time% grade% run%, $(wildcard tests/*.a))
