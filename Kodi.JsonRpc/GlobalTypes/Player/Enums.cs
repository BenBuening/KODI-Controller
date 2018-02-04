namespace Kodi.JsonRpc.GlobalTypes.Player
{

    public enum ValueProperty
    {
        type,
        partymode,
        speed,
        time,
        percentage,
        totaltime,
        playlistid,
        position,
        repeat,
        shuffled,
        canseek,
        canchangespeed,
        canmove,
        canzoom,
        canrotate,
        canshuffle,
        canrepeat,
        currentaudiostream,
        audiostreams,
        subtitleenabled,
        currentsubtitle,
        subtitles,
        live
    }

    public enum MoveDirection
    {
        left,
        right,
        up,
        down
    }

    public enum PlayRotate
    {
        clockwise,
        counterclockwise
    }

    public enum GoToTarget
    {
        previous,
        next
    }

    public enum RepeatOption
    {
        off,
        one,
        all
    }

    public enum ToggleOption
    {
        toggle
    }

    public enum SeekOption
    {
        smallforward,
        smallbackward,
        bigforward,
        bigbackward
    }

    public enum AudioStreamOption
    {
        previous,
        next
    }

    public enum PlaybackSpeed
    {
        slow32x = -32,
        slow16x = -16,
        slow8x = -8,
        slow4x = -4,
        slow2x = -2,
        slow1x = -1,
        Normal = 0,
        fast1x = 1,
        fast2x = 2,
        fast4x = 4,
        fast8x = 8,
        fast16x = 16,
        fast32x = 32
    }

    public enum SubtitleOption
    {
        previous,
        next,
        off,
        on
    }

    public enum ZoomOption
    {
        @in,
        @out
    }

}
