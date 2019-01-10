

test%: tests/%.a $(BUILD)
	@cat tests/$*
	@echo
	@echo
	@cat tests/$* | $(RUN)
	@echo
	@cat tests/$*.a
	@echo

test: test01

run%: tests/%.a $(BUILD)
	@cat tests/$* | $(RUN) > tests/$*.result

time%: tests/%.a $(BUILD)
	@echo Test $*
	@time cat tests/$* | $(RUN) > tests/$*.result
	@echo

grade%: tests/%.a $(BUILD) check/check
	@cat tests/$* | $(RUN) > tests/$*.result && check/check tests/$* tests/$*.result tests/$*.a

time:  $(patsubst tests/%.a, time%, $(wildcard tests/*.a))
grade: $(patsubst tests/%.a, grade%, $(wildcard tests/*.a))

clean: cleanup
	@rm -f tests/*.result

.PHONY: clean cleanup test time grade
