/*
 * BoardConfig.h
 * Configuration Settings for the target board of GlowProxy Alpha
 * Includes port settings, etc
 *  Author: paul
 */ 

#include <avr/io.h>

#ifndef BOARDCONFIG_H_
#define BOARDCONFIG_H_

#define BOARDCONFIG_LEDCOUNT	8

#define LED0PIN		PB4
#define LED1PIN		PC2
#define LED2PIN		PD0
#define LED3PIN		PD7
#define LED4PIN		PB0
#define LED5PIN		PB6
#define LED6PIN		PC7
#define LED7PIN		PB5

#define LED0PORT	&PORTB
#define LED1PORT	&PORTC
#define LED2PORT	&PORTD
#define LED3PORT	&PORTD
#define LED4PORT	&PORTB
#define LED5PORT	&PORTB
#define LED6PORT	&PORTC
#define LED7PORT	&PORTB

#define ENCODER_PINS			PIND
#define ENCODER_PORT			PORTD
#define ENCODER_PORTDIRECTION	DDRD

#define	ENCODER_PIN_A			PD3
#define	ENCODER_PIN_B			PD4
#define	ENCODER_SW				PD6

// Used for masking the debouncer output
static const uint8_t ENCODER_PORTMASK = (1<<ENCODER_PIN_A) | (1<<ENCODER_PIN_B) | (1 << ENCODER_SW);
static const uint8_t ENCODER_ROTATIONPINS_BITMASK = (1<<ENCODER_PIN_A) | (1<<ENCODER_PIN_B);
static const uint8_t ENCODER_BUTTON_BITMASK = (1<<ENCODER_SW);

#define RED	OCR1A // PC6
#define GRN	OCR1B // PC5
#define BLU	OCR1C // PB7


void BoardConfig_Init(void);


#endif /* BOARDCONFIG_H_ */