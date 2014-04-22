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
	Leds_SetWhite();
}

// Main task; should be called often, i.e. in a main loop
void Leds_Task(void)
{
	uint8_t i;
	
	for(i = 0; i < 8; i++)
	{
		uint8_t bit = 0xff;
		uint8_t red = currentFrame.Colors[i].Red;
		uint8_t green = currentFrame.Colors[i].Green;
		uint8_t blue = currentFrame.Colors[i].Blue;
		
		if(red == 0 && green == 0 && blue == 0)
		{
			bit = 0x00;
		}
		
		RED	= red;
		GRN = green;
		BLU = blue;
		
		//turn off last port-pin combo
		*lastPort &= ~(_BV(lastPin));
		
		if(bit > 0x00)
		{
			// Turn on the current LED
			*LedConfig_Ports[i] |= _BV(LedConfig_Pins[i]);
		}
			
			
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
	
}

void Leds_SetWhite(void)
{
	int i = 0;
	for(i = 0; i < 24; i++)
	{
		currentFrame.RawData[i] = 0xff;
	}
}

void Leds_SetColor(uint8_t index, Leds_Color color)
{
	//if(((color.Red == color.Green) == color.Blue) == 0x00)
	//{
		//currentFrame.LedState.RawData &= ~(_BV(index));
	//}
	//else
	//{
		//currentFrame.LedState.RawData |= _BV(index);	
	//}
	currentFrame.Colors[index] = color;
}

/************************************************************************/
/* PRIVATE FUNCTIONS                                                    */
/************************************************************************/

//...