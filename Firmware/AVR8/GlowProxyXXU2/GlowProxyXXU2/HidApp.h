/** \file
 *
 *  Header file for HidApp.c.
 */

#ifndef _HIDAPP_H_
#define _HIDAPP_H_

/* Includes: */
#include <avr/io.h>
#include <avr/wdt.h>
#include <avr/power.h>
#include <avr/interrupt.h>
#include <string.h>

#include "Descriptors.h"
#include "Config/AppConfig.h"
#include "Config/BoardConfig.h"
#include "Drivers/Encoder.h"
#include "Drivers/Leds.h"

#include <LUFA/Drivers/USB/USB.h>
#include <LUFA/Platform/Platform.h>


/* Structures */
	
typedef union {
	uint8_t RawData[24];
	struct
	{
		uint8_t ButtonState;
		uint8_t Left;
		uint8_t Right;
		uint8_t Debug[21];
	};
} ATTR_PACKED GlowProxy_OutputReport;

typedef union {
	uint8_t RawData[24];
	struct
	{
		Leds_Color Leds[8];
	};
} ATTR_PACKED GlowProxy_InputReport;

	
/* Enumerations */

//// The firmware will be in one of the following application states at any given time
//typedef enum
//{
	//ApplicationMode_UsbOffline = 0x00,
	//ApplicationMode_UsbConnected = 0x01,
	//ApplicationMode_UsbActive = 0x02,
//
	//ApplicationMode_DeviceOff = 0xfd,  // primarily used as an initial state from which to switch
	//ApplicationMode_UsbErrorState = 0xfe,
	//ApplicationMode_GenericErrorState = 0xff
//} Application_ModeOptions;


/* Function Prototypes: */
void HidApp_Init(void);

void EVENT_USB_Device_Connect(void);
void EVENT_USB_Device_Disconnect(void);
void EVENT_USB_Device_ConfigurationChanged(void);
void EVENT_USB_Device_ControlRequest(void);
void EVENT_USB_Device_StartOfFrame(void);

bool CALLBACK_HID_Device_CreateHIDReport(USB_ClassInfo_HID_Device_t* const	HIDInterfaceInfo,
                                                            uint8_t* const	ReportID,
                                                            const uint8_t	ReportType,
                                                            void*			ReportData,
                                                            uint16_t* const ReportSize);

void CALLBACK_HID_Device_ProcessHIDReport(USB_ClassInfo_HID_Device_t* const HIDInterfaceInfo,
															const uint8_t	ReportID,
															const uint8_t	ReportType,
															const void*		ReportData,
															const uint16_t	ReportSize);

#endif

