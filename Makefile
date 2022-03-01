SHELL := /bin/bash


.PHONY: exe
exe:
	csc AccountHierarchyTest.cs Account.cs CheckingAccount.cs SavingsAccount.cs
	csc AccountTest.cs Account.cs CheckingAccount.cs SavingsAccount.cs


.PHONY: output
output: exe
	mono AccountHierarchyTest.exe | tee AccountHierarchyTestOutput.txt
	mono AccountTest.exe | tee AccountTestOutput.txt


.PHONY: test
test: output
	diff expected_out1.txt AccountHierarchyTestOutput.txt
	diff expected_out2.txt AccountTestOutput.txt


.PHONY: help
help:
	@printf "available targets -->\n\n"
	@cat Makefile | grep ".PHONY" | grep -v ".PHONY: _" | sed 's/.PHONY: //g'

