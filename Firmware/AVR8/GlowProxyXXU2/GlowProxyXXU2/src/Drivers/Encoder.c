/*
 * Manages Application Encoder
 *  Author: Paul Trandem
 */ 

#include <stdbool.h>
#include "Encoder.h"
#include "Config/BoardConfig.h"

#define DEBOUNCE_CHECKS		5

static void DebounceInputs(void);

volatile static uint8_t debouncedInputState;
volatile static uint8_t lastDebouncedInputState;
volatile static uint8_t inputState[DEBOUNCE_CHECKS];
volatile static uint8_t inputStateIndex = 0;

//This will contain the debounced state of all inputs on the down (press) edge
volatile static uint8_t buttonDown;

//This will contain the debounced state of all inputs on the up (release) edge
volatile static uint8_t buttonUp;

static void (*EVENT_ButtonDown)(uint8_t buttonMask);
static void (*EVENT_ButtonUp)(uint8_t buttonMask);
static void (*EVENT_EncoderCW)(void);
static void (*EVENT_EncoderCCW)(void);

void Encoder_Init(	void (*buttonDownEvent)(uint8_t buttonMask),
					void (*buttonUpEvent)(uint8_t buttonMask),
					void (*encoderClockwiseEvent)(void),
					void (*encoderCounterClockwiseEvent)(void))
{
	EVENT_ButtonDown = buttonDownEvent;
	EVENT_ButtonUp = buttonUpEvent;
	EVENT_EncoderCW = encoderClockwiseEvent;
	EVENT_EncoderCCW = encoderCounterClockwiseEvent;
}

Encoder_EncoderRotation rotationState = EncoderRotation_Zero;
bool pinDown = false;
uint8_t expectedNextCC = 0x08;
uint8_t expectedNextCCW = 0x10;

void Encoder_Task()
{
	DebounceInputs();
	if(buttonDown > 0)
	{
		// TODO: make this more concise with a lookup table; clean it up and tighten it up.
		
		uint8_t pins = buttonDown & ENCODER_ROTATIONPINS_BITMASK;
		if(pins > 0)
		{
			if(pins == expectedNextCC)
			{
				//going clockwise
				switch(expectedNextCC)
				{
					case 0x08:
						expectedNextCC = 0x10;
						break;
						
					case 0x10:
						expectedNextCC = 0x00;
						break;
				}

				rotationState = EncoderRotation_CC;
				//if(EVENT_EncoderCW > 0)
				//{
					//EVENT_EncoderCW();
				//}
			}
			else if(pins == expectedNextCCW)
			{
				//going Counter-Clockwise
				switch(expectedNextCCW)
				{
					case 0x010:
						expectedNextCCW = 0x08;
						break;
					
					case 0x08:
						expectedNextCCW = 0x00;
						break;
				}
				rotationState = EncoderRotation_CCW;
				
				//if(EVENT_EncoderCCW > 0)
				//{
					//EVENT_EncoderCCW();
				//}
			}
			else
			{
				//If we slip into an unknown state, reset and get our bearings
				rotationState = EncoderRotation_Zero;
				expectedNextCC = 0x08;
				expectedNextCCW = 0x10;
			}
		}

		EVENT_ButtonDown(buttonDown & ENCODER_BUTTON_BITMASK);
	}
	
	if(buttonUp > 0)
	{
		uint8_t pins = buttonUp & ENCODER_ROTATIONPINS_BITMASK;
		if(pins > 0)
		{
			if(rotationState == EncoderRotation_CC && expectedNextCC == 0x00)
			{
				rotationState = EncoderRotation_Zero;
				expectedNextCC = 0x08;
				if(EVENT_EncoderCW > 0)
				{
					EVENT_EncoderCW();
				}
			}
			else if(rotationState == EncoderRotation_CCW && expectedNextCCW == 0x00)
			{
				rotationState = EncoderRotation_Zero;
				expectedNextCCW = 0x10;
				if(EVENT_EncoderCCW > 0)
				{
					EVENT_EncoderCCW();
				}
			}
		}
		
		EVENT_ButtonUp(buttonUp & ENCODER_BUTTON_BITMASK);
	}
}

static void DebounceInputs(void)
{
	uint8_t i, j;
	
	//Get the port state
	inputState[inputStateIndex] = ~(ENCODER_PINS);
	++inputStateIndex;
	j = 0xff;
	
	for(i=0; i<DEBOUNCE_CHECKS; i++) j = j & inputState[i];
	
	//The bit is only set in the debounced variable if it has been on for all iterations of DEBOUNCE_CHECKS
	// (meaning it's now clean and properly debounced)
	// The output bits we don't actually care about are &'d out using the BUTTONPORTMASK
	lastDebouncedInputState = debouncedInputState;
	debouncedInputState = (j & ENCODER_PORTMASK);
	
	// Isolate transitions
	buttonDown = debouncedInputState & ~lastDebouncedInputState;
	buttonUp = ~debouncedInputState & lastDebouncedInputState;
	
	//circular
	if(inputStateIndex >= DEBOUNCE_CHECKS) inputStateIndex = 0;
}