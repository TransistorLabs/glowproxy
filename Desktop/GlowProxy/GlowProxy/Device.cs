using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GlowProxyAPI.Net;

namespace GlowProxy
{
    public class Device
    {
        public int CurrentIndex { get; set; }

        private byte _lastRawInput;        
        private byte _red, _green, _blue;
        private byte _cycleModeState;
        private IntPtr _handle;
        private readonly LedState _currentFrame;
        private readonly LedState _whiteFrame;
        private readonly LedState _darkFrame;
        private bool _isRunning;
        private InputReport _lastReport;

        public Action OnClick;
        public Action<Color> OnColorChange;

        public Device()
        {
            _isRunning = false;
            _currentFrame = new LedState();
            _whiteFrame = new LedState();
            _darkFrame = new LedState();
            
            var white = new Color("#ffffff");
            var dark = new Color("#000000");
            for (var i = 0; i < 8; i++)
            {
                _whiteFrame.Leds[i] = white;
                _darkFrame.Leds[i] = dark;
            }

        }

        public void Open()
        {
            _handle = GlowProxyAPI.Net.GlowProxy.Open();
        }

        CancellationTokenSource cts;
        public async void StartProcessingInput()
        {
            _isRunning = true;
            while (_isRunning)
            {
                await InputTask();
            }
        }

        public void StopProcessingInput()
        {
            _isRunning = false;

        }

        public void Close()
        {
            _isRunning = false;
            GlowProxyAPI.Net.GlowProxy.Close(_handle);
        }

        private async Task InputTask()
        {
            var report = new InputReport();
            _lastReport = report;
            
            await Task.Run(() => GlowProxyAPI.Net.GlowProxy.ReadInput(_handle, ref report));
            if (!_lastReport.Equals(report))
            {
                var color = GetColorFromInput(report.CurrentCount);
                SetSelfColor(color);
                if (OnColorChange != null)
                {
                    await Task.Run(() => OnColorChange(color));
                }
                if (report.ButtonMask > 0)
                {
                    if (OnClick != null)
                    {
                        await Task.Run(() => OnClick());
                    }
                }
            }
        }

        public Color GetColorFromInput(byte deviceInput)
        {
            //TODO: make this algorithm not suck...
            _red = _green = _blue = _cycleModeState = 0;
            const byte increment = 7;
            for (var i = 0; i < deviceInput; i++)
            {
                switch (_cycleModeState)
                {
                    case 0:
                        if (_red < 0xff)
                        {
                            _red += increment;
                        }
                        else
                        {
                            _cycleModeState = 1;
                        }
                        break;

                    case 1:
                        if (_green < 0xff)
                        {
                            _green += increment;
                        }
                        else
                        {
                            _cycleModeState = 2;
                        }
                        break;

                    case 2:
                        if (_red > 0x00)
                        {
                            _red -= increment;
                        }
                        else
                        {
                            _cycleModeState = 3;
                        }
                        break;

                    case 3:
                        if (_blue < 0xff)
                        {
                            _blue += increment;
                        }
                        else
                        {
                            _cycleModeState = 4;
                        }
                        break;

                    case 4:
                        if (_green > 0x00)
                        {
                            _green -= increment;
                        }
                        else
                        {
                            _cycleModeState = 5;
                        }
                        break;

                    case 5:
                        if (_red < 0xff)
                        {
                            _red += increment;
                        }
                        else
                        {
                            _cycleModeState = 6;
                        }
                        break;

                    case 6:
                        if (_blue > 0x00)
                        {
                            _blue -= increment;
                        }
                        else
                        {
                            _cycleModeState = 1;
                        }
                        break;
                }
            }
            var color = new Color(_red, _green, _blue);
            return color;
        }

        public void SetSelfColor(Color color)
        {
            if (_handle == IntPtr.Zero)
            {
                return;
            }

            for (var i = 0; i < 4; i++)
            {
                _currentFrame.Leds[i] = color;
            }
            GlowProxyAPI.Net.GlowProxy.SetColor(_handle, _currentFrame);
        }

        public void SetRemoteColor(Color color)
        {
            if (_handle == IntPtr.Zero)
            {
                return;
            }

            for (var i = 4; i < 8; i++)
            {
                _currentFrame.Leds[i] = color;
            }
            GlowProxyAPI.Net.GlowProxy.SetColor(_handle, _currentFrame);
        }

        public async void BlinkWhite(int count)
        {
            if (_handle == IntPtr.Zero)
            {
                return;
            }

            StopProcessingInput();

            for (var i = 0; i < count; i++)
            {
                GlowProxyAPI.Net.GlowProxy.SetColor(_handle, _whiteFrame);
                await Task.Delay(200);
                GlowProxyAPI.Net.GlowProxy.SetColor(_handle, _darkFrame);
                await Task.Delay(200);
            }
            GlowProxyAPI.Net.GlowProxy.SetColor(_handle, _currentFrame);
            StartProcessingInput();
        }
    }
}
