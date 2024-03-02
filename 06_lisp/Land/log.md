# log

## 動作確認

```lisp
[1]>
:q
:Q
[2]> (+ 2 1)
3
[3]> (+ 2 ( * 3 4))
14
```

## トップレベル定義

- グローバルに定義される変数のこと
  - `defparameter`：再定義可能
  - `defvar`：再定義不可

```lisp
[4]> (defparameter *small* 1)
*SMALL*
[5]> (defparameter *big* 100)
*BIG*
[6]>
```

## 関数の定義

- `defun`

```lisp
[8]> (defun guess-my-number()
        (ash(+ *small* *big*) -1))
GUESS-MY-NUMBER
```

## ash

- 11(1011) を 1 だけ左にシフトすると 22(10110)
- 11(1011) を 1 だけ右にシフトすると 5(101)

```lisp
[8]> (ash 11 1)
22
[8]> (ash 11 -1)
5
```

## 必要な関数

```lisp
[16]> (defun smaller ()
        (setf *big* (1- (guess-my-number)))
        (guess-my-number))
[17]> (defun bigger ()
        (setf *small* (1+ (guess-my-number)))
        (guess-my-number))
BIGGER
```

```lisp
[18]> (defun start-over ()
        (defparameter *small* 1)
        (defparameter *big* 100)
        (guess-my-number))
START-OVER
```

## 動作確認2

- `30`とする

```lisp
> (start-over)
50
> (smaller)
25
> (bigger)
37
> (smaller)
31
> (smaller)
28
> (bigger)
29
> (bigger)
30
```

## ローカル変数

```lisp
> (let ((a 5)
        (b 6))
      (+ a b))
11
```

## ローカル関数

```lisp
> (flet ((f (n)
              (+ n 10)))
            (f 5))
15
```

## メモ

- Lispは、単純なsyntaxを持つ
