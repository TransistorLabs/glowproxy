/*
 * Header for Encoder.c
 *
 *  Author: Paul Trandem
 */ 


#ifndef ENCODER_H_
#define ENCODER_H_

#include <avr/io.h>

typedef enum
{
	EncoderRotation_Zero = 0x00,
	EncoderRotation_CC = 0x01,
	EncoderRotation_CCW = 0x02,
} Encoder_EncoderRotation;

void Encoder_Init(	void (*buttonDownEvent)(uint8_t buttonMask), 
					void (*buttonUpEvent)(uint8_t buttonMask), 
					void (*encoderClockwiseEvent)(void), 
					void (*encoderCounterClockwiseEvent)(void));
					
void Encoder_Task(void);

#endif /* ENCODER_H_ */