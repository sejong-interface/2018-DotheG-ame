## nuchuk 모듈 기능 명세서

#### 함수

1. int nunchuk_read(void)  
>nunchuk 모듈의 데이터 값을 읽는 함수

2. void nunchuk_init(void)  
> nunchuk 모듈 초기화 및 I2C 통신 초기화

3. int nunchuk_buttonZ(void)  
> nunchuk 모듈의 Z버튼의 상태는 읽는 함수

4. int nunchuk_buttonC(void)  
> nunchuk 모듈의 C버튼의 상태를 읽는 함수

5. int nunchuk_joystickX(void)  
> 조이스틱의 X축 값을 읽는 함수 (0 ~ 256)

6. int nunchuk_joystickY(void)  
> 조이스틱의 Y축 값을 읽는 함수 (0 ~ 256)

7. int move_joystick(int thisAxis)
> 조이스틱의 변화값(0~256) -> (0~12) 로 변환