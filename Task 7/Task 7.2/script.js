const mathCalculator = (string) => {
  const operatorsSym = ['+', '-', '*', '/', '='];
  const operands = [];
  const operators = [];

  string.split('').reduce((acc, sym) => {
    if (operatorsSym.includes(sym)) {
      operators.push(sym);
      operands.push(Number(acc.trim()));
      acc = '';
    } else acc += sym;
    return acc;
  }, '');

  return Number(
    operands
      .slice(1, operands.length)
      .reduce((acc, num, idx) => {
        switch (operators[idx]) {
          case '+':
            return (acc += num);
          case '-':
            return (acc -= num);
          case '*':
            return (acc *= num);
          case '/':
            return (acc /= num);
          case '=':
            return acc;
        }
      }, operands[0])
      .toFixed(2)
  );
};

mathCalculator('3.5 +4*10-5.3 /5 =');
