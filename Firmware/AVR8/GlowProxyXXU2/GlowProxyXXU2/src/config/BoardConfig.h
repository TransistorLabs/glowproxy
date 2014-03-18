/*
 * BoardConfig.h
 * Configuration Settings for the target board of GlowProxy Alpha
 * Includes port settings, etc
 *  Author: paul
 */ 

#include <avr/io.h>

#ifndef BOARDCONFIG_H_
#define BOARDCONFIG_H_

#define ENCODER_PINS			PIND
#define ENCODER_PORT			PORTD
#define ENCODER_PORTDIRECTION	DDRD

#define	ENCODER_PIN_A			PD3
#define	ENCODER_PIN_B			PD4
#define	ENCODER_SW				PD5

// Used for masking the debouncer output
static const uint8_t ENCODER_PORTMASK = (1<<ENCODER_PIN_A) | (1<<ENCODER_PIN_B) | (1 << ENCODER_SW);
static const uint8_t ENCODER_ROTATIONPINS_BITMASK = (1<<ENCODER_PIN_A) | (1<<ENCODER_PIN_B);
static const uint8_t ENCODER_BUTTON_BITMASK = (1<<ENCODER_SW);

#define RED	OCR1A // PC6
#define GRN	OCR1B // PC5
#define BLU	OCR1C // PB7


void BoardConfig_Init(void);


#endif /* BOARDCONFIG_H_ */