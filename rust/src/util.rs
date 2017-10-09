use std::io::*;
use std::char;
use std::str::FromStr;

pub trait Tokens: Iterator<Item = String> {
    fn get_string(&mut self) -> String {
        self.next().expect("Unexpected EOF")
    }

    fn get<T: FromStr>(&mut self) -> T {
        let s = self.get_string();
        if let Ok(v) = s.parse() {
            v
        } else {
            panic!("Unexpected token: {}", s);
        }
    }

    fn get_vec<T: FromStr>(&mut self, len: usize) -> Vec<T> {
        (0..len).map(|_| self.get()).collect()
    }
}


impl <I: Iterator<Item = String>> Tokens for I {}

pub struct Tokenizer<I> {
    chars: I,
    buf: String
}

impl <I: Iterator<Item = char>> Iterator for Tokenizer<I> {
    type Item = String;

    fn next(&mut self) -> Option<String> {
        while let Some(c) = self.chars.next() {
            if !c.is_whitespace() {
                self.buf.push(c);
                break;
            }
        }
        while let Some(c) = self.chars.next() {
            if c.is_whitespace() {
                break;
            } else {
                self.buf.push(c);
            }
        }
        if self.buf.len() > 0 {
            let tok = String::from(self.buf.as_str());
            self.buf.clear();
            Some(tok)
        } else {
            None
        }
    }
}

pub struct CharsFromBytes<R> {
    bytes: Bytes<R>
}

impl <R: Read> Iterator for CharsFromBytes<R> {
    type Item = char;

    fn next(&mut self) -> Option<char> {
        if let Some(io) = self.bytes.next() {
            let b = io.unwrap();
            let c = char::from_u32(b as u32).expect("None ASCII byte found");
            Some(c)
        } else {
            None
        }
    }
}

impl <R: BufRead> From<R> for Tokenizer<CharsFromBytes<R>> {
    fn from(r: R) -> Self {
        Tokenizer {
            buf: String::new(),
            chars: CharsFromBytes { bytes: r.bytes() }
        }
    }
}

impl <'a> From<&'a Stdin> for Tokenizer<CharsFromBytes<StdinLock<'a>>> {
    fn from(stdin: &'a Stdin) -> Self {
        Tokenizer::from(stdin.lock())
    }
}
