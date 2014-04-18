/*
 * Leds.c
 *
 * Created: 4/13/2014 8:27:29 PM
 *  Author: paul
 */ 

#include <util/delay.h>
#include "Leds.h"
#include "config/BoardConfig.h"

static const uint8_t LedConfig_Pins[BOARDCONFIG_LEDCOUNT]
		= {	LED0PIN,    LED1PIN,    LED2PIN,    LED3PIN,
			LED4PIN,    LED5PIN,    LED6PIN,    LED7PIN};

volatile static uint8_t *LedConfig_Ports[BOARDCONFIG_LEDCOUNT]
		= {	LED0PORT,   LED1PORT,   LED2PORT,   LED3PORT,
			LED4PORT,   LED5PORT,   LED6PORT,   LED7PORT};

static Leds_Frame currentFrame;
static uint8_t colorSplitThreshold;

volatile static uint8_t *lastPort =	LED0PORT;
static uint8_t  lastPin =	LED0PIN;


/************************************************************************/
/* PRIVATE FUNCTION DECLARATIONS                                        */
/************************************************************************/
//...


/************************************************************************/
/* PUBLIC FUNCTION IMPLEMENTATIONS                                      */
/************************************************************************/

// Driver initialization
void Leds_Init(void)
{
	lastPort = LED0PORT;
	lastPin =	LED0PIN;
	
	/* TODO: 2 is a magic number used through-out; let's abstract this into a global 
		setting, so we can go up to BOARDCONFIG_LEDCOUNT colors, and possibly define this
		via USB feature setting
	*/
	colorSplitThreshold = BOARDCONFIG_LEDCOUNT/2;
	Leds_SetWhite();
	Leds_SetColor(0, (Leds_Color){.Blue = 0xff});
	Leds_SetColor(1, (Leds_Color){.Green = 0xff});
}

// Main task; should be called often, i.e. in a main loop
void Leds_Task(void)
{
	uint8_t i;
	
	for(i = 0; i < BOARDCONFIG_LEDCOUNT; i++)
	{
		uint8_t bit = currentFrame.LedState.RawData;
		
		if(i < colorSplitThreshold)
		{
			RED	= currentFrame.Colors[0].Red;
			GRN = currentFrame.Colors[0].Green;
			BLU = currentFrame.Colors[0].Blue;
		}
		else
		{
			RED	= currentFrame.Colors[1].Red;
			GRN = currentFrame.Colors[1].Green;
			BLU = currentFrame.Colors[1].Blue;
		}
		
		bit &= _BV(i);
		if(bit != 0x00)
		{
			// Turn on the current LED
			*LedConfig_Ports[i] |= _BV(LedConfig_Pins[i]);
		}
			
		//turn off last port-pin combo
		*lastPort &= ~(_BV(lastPin));
			
		//Set new last port-pin combo
		lastPort = LedConfig_Ports[i];
		lastPin = LedConfig_Pins[i];

		// TODO: add some microsecond delay here or not?
		_delay_us(200);
	}
	
	//turn off final port-pin combo
	*lastPort &= ~(_BV(lastPin));
}

void Leds_Clear(void)
{
	currentFrame = (Leds_Frame) {
		.Colors[0].Red		= 0x00,
		.Colors[0].Green	= 0x00,
		.Colors[0].Blue		= 0x00,
		.Colors[1].Red		= 0x00,
		.Colors[1].Green	= 0x00,
		.Colors[1].Blue		= 0x00,
		.LedState = (Leds_LedState) { .RawData = 0x00 }
	};
}

void Leds_SetWhite(void)
{
	currentFrame = (Leds_Frame) {
		.Colors[0].Red		= 0xff,
		.Colors[0].Green	= 0xff,
		.Colors[0].Blue		= 0xff,
		.Colors[1].Red		= 0xff,
		.Colors[1].Green	= 0xff,
		.Colors[1].Blue		= 0xff,
		.LedState = (Leds_LedState) { .RawData = 0xff }
	};
}

void Leds_SetColor(uint8_t index, Leds_Color color)
{
	currentFrame.Colors[index] = color;
}

/************************************************************************/
/* PRIVATE FUNCTIONS                                                    */
/************************************************************************/

//...