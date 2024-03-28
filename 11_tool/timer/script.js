let timeLeft = 60;
let timerInterval = null;
const timerElement = document.getElementById('timer');
const timerMessageElement = document.getElementById('timerMessage'); // メッセージ表示用の要素を取得
const startButton = document.getElementById('startButton');
const pauseButton = document.getElementById('pauseButton');
const resetButton = document.getElementById('resetButton');

function formatTime(seconds) {
  const minutes = Math.floor(seconds / 60);
  const remainingSeconds = seconds % 60;
  return `${pad(minutes)}:${pad(remainingSeconds)}`;
}

function pad(number) {
  return number < 10 ? '0' + number : number.toString();
}

function showTimerMessage(message) {
  // 既にメッセージがあれば削除
  const existingMessage = document.getElementById('timerMessage');
  if (existingMessage) {
    existingMessage.parentNode.removeChild(existingMessage);
  }

  // 新しいメッセージ用のdivを作成
  const messageDiv = document.createElement('div');
  messageDiv.id = 'timerMessage';
  messageDiv.textContent = message;
  document.body.appendChild(messageDiv); // bodyの最後にメッセージを追加
}

function updateTimerDisplay(seconds) {
  timerElement.textContent = formatTime(seconds);
  if (seconds <= 10) {
    timerElement.classList.add('timer-alert');
  } else {
    timerElement.classList.remove('timer-alert');
  }
}

function startTimer() {
  if (!timerInterval) {
    timerInterval = setInterval(() => {
      timeLeft -= 1;
      updateTimerDisplay(timeLeft);
      if (timeLeft <= 0) {
        clearInterval(timerInterval);
        timerInterval = null;
        showTimerMessage('Next！！');
        pauseButton.disabled = true;
      }
    }, 1000);
    pauseButton.disabled = false;
  }
}

function pauseTimer() {
  if (timerInterval) {
    clearInterval(timerInterval);
    timerInterval = null;
    pauseButton.disabled = true;
  }
}

function resetTimer() {
  if (timerInterval) {
    clearInterval(timerInterval);
    timerInterval = null;
  }
  timeLeft = 60;
  updateTimerDisplay(timeLeft);
  pauseButton.disabled = true;
  timerElement.classList.remove('timer-alert');

  // 'タイマー終了' のメッセージをクリア
  const timerMessageElement = document.getElementById('timerMessage');
  if (timerMessageElement) timerMessageElement.textContent = '';
}

startButton.addEventListener('click', startTimer);
pauseButton.addEventListener('click', pauseTimer);
resetButton.addEventListener('click', resetTimer);

updateTimerDisplay(timeLeft);
