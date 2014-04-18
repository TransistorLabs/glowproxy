/*
 * Leds.h
 *
 * Created: 4/11/2014 10:56:16 PM
 *  Author: paul
 */ 


#ifndef LEDS_H_
#define LEDS_H_

#include <avr/common.h>
#include <stdbool.h>
	
/* Data Structures */
	
typedef union
{
	struct
	{
		uint8_t		Led0	: 1,
					Led1	: 1,
					Led2	: 1,
					Led3	: 1,
					Led4	: 1,
					Led5	: 1,
					Led6	: 1,
					Led7	: 1;
	};
	uint8_t RawData;
} Leds_LedState;

typedef struct  
{
	uint8_t Red;
	uint8_t Green;
	uint8_t Blue;
} Leds_Color;
	
typedef struct {
	Leds_Color Colors[2];
	Leds_LedState LedState;
} Leds_Frame;
	

	
/* Function Prototypes */

// Initialize the driver
void Leds_Init(void);
	
// Should be called in a main loop, as often as possible to ensure rendering occurs correctly
void Leds_Task(void);

// Clear (blank) the display
void Leds_Clear(void);
	
// Set display to EFO (Everything Fully On)
void Leds_SetWhite(void);

// Set color sets
void Leds_SetColor(uint8_t index, Leds_Color color);

	
	
#endif /* LEDS_H_ */