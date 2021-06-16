const charRemover = (string) => {
  const repits = new Set();
  return string
    .split(' ')
    .map((word) => {
      return word
        .trim()
        .split('')
        .map((letter) => {
          if (word.split(letter).length - 1 >= 2) repits.add(letter);
          return letter;
        })
        .join('');
    })
    .map((word) => {
      return word
        .split('')
        .filter((letter) => !repits.has(letter))
        .join('');
    })
    .join(' ');
};

console.log(charRemover(`У попа была собака`));
