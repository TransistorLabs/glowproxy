/*
 * BoardConfig.c
 *  Author: Paul Trandem
 */ 

#include "TransistorLabs/avr.h"
#include "Config/BoardConfig.h"

void BoardConfig_Init(void)
{
	/*
		The ATmegaxxU2 series has two timers, Timer0 and Timer1.  
		Timer1 features three 8-bit PWM outputs, so we'll use that one for all PWM needs.
	*/
	
	// Configure Timer1 for Fast PWM 8-bit
	TCCR1A |= _BV(WGM10);
	TCCR1B |= _BV(WGM12);
	
	// Turn on OC1A - inverted
	TCCR1A |=  _BV(COM1A1) | _BV(COM1A0);
	TCCR1A |=  _BV(COM1B1) | _BV(COM1B0);
	TCCR1A |=  _BV(COM1C1) | _BV(COM1C0);
	
	RED = 0x0000;
	GRN = 0x0000;
	BLU = 0x0000;
	

	// Configure port outputs (1)
	//DDRC |= _BV(PC2) | _BV(PC4) | _BV(PC5) | _BV(PC6) | _BV(PC7);
	//DDRB |= _BV(PB0) | _BV(PB4) | _BV(PB5) | _BV(PB6) | _BV(PB7);
	//DDRD |= _BV(PD0) | _BV(PD4) | _BV(PD5) | _BV(PD1) | _BV(PD7);
	
	// enable PWM - using system clock, no prescaler
	TCCR1B |= _BV(CS10);
	
	// Configure the input hardware
	// Configure switch 1 as input (DDRx = 0)
	SETPINDIRECTION_INPUT(ENCODER_PORTDIRECTION, ENCODER_PIN_A);
	SETPINDIRECTION_INPUT(ENCODER_PORTDIRECTION, ENCODER_PIN_B);
	SETPINDIRECTION_INPUT(ENCODER_PORTDIRECTION, ENCODER_SW);
	
	
	// Turn on internal pull-up (1)
	SETINTERNALPULLUP_ON(ENCODER_PORT, ENCODER_PIN_A );
	SETINTERNALPULLUP_ON(ENCODER_PORT, ENCODER_PIN_B );
	SETINTERNALPULLUP_ON(ENCODER_PORT, ENCODER_SW );
	
}
