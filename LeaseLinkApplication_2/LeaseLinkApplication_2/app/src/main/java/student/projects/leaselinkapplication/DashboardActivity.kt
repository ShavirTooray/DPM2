package student.projects.leaselinkapplication

import android.content.Intent
import android.os.Bundle
import android.widget.ImageButton
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity

class DashboardActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_dashboard)

        // Greeting (matches @id/textView7)
        findViewById<TextView>(R.id.textView7)?.let { tv ->
            val prefs = getSharedPreferences("LeaseLinkPrefs", MODE_PRIVATE)
            val name = prefs.getString("displayName", null)
            if (!name.isNullOrBlank()) tv.text = "Hi $name"
        }

        // Action tiles (match activity_dashboard.xml IDs)
        val submitBtn  = findViewById<ImageButton>(R.id.btnSubmit)
        val paymentBtn = findViewById<ImageButton>(R.id.btnPayment)
        val chatbotBtn = findViewById<ImageButton>(R.id.btnChatbot)
        val uploadBtn  = findViewById<ImageButton>(R.id.btnUpload)

        submitBtn.setOnClickListener {
            startActivity(Intent(this, MaintenanceRequestActivity::class.java))
        }

        paymentBtn.setOnClickListener {
            startActivity(Intent(this, PaymentsActivity::class.java))
        }

        chatbotBtn.setOnClickListener {
            startActivity(Intent(this, ChatBotActivity::class.java))
        }

        // Uncomment when UploadProofActivity exists
        // uploadBtn.setOnClickListener {
        //     startActivity(Intent(this, UploadProofActivity::class.java))
        // }
    }
}





